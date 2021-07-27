using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Chatbot1.Greeting;

namespace Chatbot1
{
    public class GreetingIntentProcessor : AbstractIntentProcessor
    {
        public const string CHOICE_SLOT = "UserChoice";
        public const string INVOCATION_SOURCE = "invocationSource";
        UserChoices _choices = UserChoices.Null;
        public override LexResponse Process(LexEvent lexEvent, ILambdaContext context)
        {
            IDictionary<string, string> slots = lexEvent.CurrentIntent.Slots;
            IDictionary<string, string> sessionAttributes = lexEvent.SessionAttributes ?? new Dictionary<string, string>();
            

            //if all the values in the slots are empty.....
            if (slots.All(x => x.Value == null))
            {
                return Delegate(sessionAttributes, slots);
                
            }


            return Close(

                        sessionAttributes,
                        "Fulfilled",
                        null
                        );

                      //Response cards for user options
                      //Not implemented



                        

                        //            //new LexResponse.LexResponseCard
                        //            //{

                        //            //    ContentType = "application/vnd.amazonaws.card.generic",
                        //            //    Version = 1,
                        //            //    GenericAttachments =
                        //            //    {

                        //            //        new LexResponse.LexGenericAttachments
                        //            //        {
                        //            //            Buttons =
                        //            //            {
                        //            //                new LexResponse.LexButton
                        //            //                {
                        //            //                    Text = "Public Holidays",
                        //            //                    Value = "PublicHolidays"
                        //            //                },
                        //            //                new LexResponse.LexButton
                        //            //                {
                        //            //                    Text = "Employee Directory",
                        //            //                    Value = "EmployeeDirectory"
                        //            //                },
                        //            //                new LexResponse.LexButton
                        //            //                {
                        //            //                    Text = "Retirement",
                        //            //                    Value = "Retirement"
                        //            //                }
                        //            //            },


                        //            //            Title = null,
                        //            //            ImageUrl = null
                        //            //        }
                        //            //    },

                        //            //}
                                 


        }
      
    };
}





