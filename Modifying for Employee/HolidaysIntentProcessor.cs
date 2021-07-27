
  
using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;
using System;
using System.Collections.Generic;
using System.Text;
using Nager.Date;
using Nager.Date.Model;
using System.Linq;
using static Chatbot1.Holiday;

namespace Chatbot1
{
    public class HolidaysIntentProcessor : AbstractIntentProcessor
    {
        public const string YEAR_SLOT = "TheYear";
        public const string INVOCATION_SOURCE = "invocationSource";

        /// <summary>
        /// Performs dialog management and fulfillment for ordering flowers.
        /// 
        /// Beyond fulfillment, the implementation for this intent demonstrates the following:
        /// 1) Use of elicitSlot in slot validation and re-prompting
        /// </summary>
        /// <param name="lexEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            IDictionary<string, string> slots = lexEvent.CurrentIntent.Slots;
            IDictionary<string, string> sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();

            //if all the values in the slots are empty return the delegate, theres nothing to validate or do.
            if (slots.All(x => x.Value == null))
            {
                return Delegate(sessionAttributes, slots);
            }

            Holiday holiday = CreateHoliday(slots);

            if (string.Equals(lexEvent.InvocationSource, "DialogCodeHook", StringComparison.Ordinal))
            {
                //validate the remaining slots.
                var validateResult = Validate(holiday);
                // If any slots are invalid, re-elicit for their value


                if (!validateResult.IsValid)
                {
                    slots[validateResult.ViolationSlot] = null;
                    return ElicitSlot(sessionAttributes, lexEvent.CurrentIntent.Name, slots, validateResult.ViolationSlot, validateResult.Message);
                }

                return Delegate(sessionAttributes, slots);
            }

            // function call
            var output = HolidayLookup(holiday.TheYear);

            return Close(
                        sessionAttributes,
                        "Fulfilled",
                        new LexResponse.LexMessage
                        {
                            ContentType = MESSAGE_CONTENT_TYPE,
                            Content = String.Format($"Here are the upcoming holidays in {holiday.TheYear}:\n {output}\n "),



                        }
                        );


        }

        private Holiday CreateHoliday(IDictionary<string, string> slots)
        {
            
            Holiday holiday = new Holiday()
            {
                TheYear = slots.ContainsKey(YEAR_SLOT) ? slots[YEAR_SLOT] : null,
                
            };

            return holiday;
        }
        private ValidationResult Validate(Holiday holiday)
        {

            if (!string.IsNullOrEmpty(holiday.TheYear))
            {
                var input = holiday.TheYear.Length;
                var someyear = holiday.TheYear;
                var nextyear = DateTime.Today.AddYears(1).Year;

                if (input != 4)
                {
                    return new ValidationResult(false, YEAR_SLOT, "Please re-enter the year in a YYYY number format");
                }

                if (Int32.Parse(someyear) < DateTime.Today.Year)
                {
                    return new ValidationResult(false, YEAR_SLOT, "Please enter a valid time frame");
                }

                if (Int32.Parse(someyear) > nextyear)
                {
                    return new ValidationResult(false, YEAR_SLOT, "Please enter a valid time frame");
                }

            }
            return ValidationResult.VALID_RESULT;
        }
        private string HolidayLookup(string yearEntered)
        {
            var holz = new Holiday();
            return holz.HolidayCalc(Int32.Parse(yearEntered));    
        }


    }
}
