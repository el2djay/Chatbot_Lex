using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Chatbot1.Services;

namespace Chatbot1
{
    public class EmployeeIntentProcessor : AbstractIntentProcessor
    {
       
        public const string LAST_NAME_SLOT = "LastName";
        public const string FIRST_NAME_SLOT = "FirstName";
        public const string ROLE_SLOT = "Role";
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

            EmployeeLookUp employee = CreateEmployeeLookUp(slots);

            if (string.Equals(lexEvent.InvocationSource, "DialogCodeHook", StringComparison.Ordinal))
            {
                //validate the remaining slots.
                var validateResult = Validate(employee);
                // If any slots are invalid, re-elicit for their value


                if (!validateResult.IsValid)
                {
                    slots[validateResult.ViolationSlot] = null;
                    return ElicitSlot(sessionAttributes, lexEvent.CurrentIntent.Name, slots, validateResult.ViolationSlot, validateResult.Message);
                }


                return Delegate(sessionAttributes, slots);
            }

           

            var searchLastName = LastNameLookup(employee.LastName);
            var searchFirstName = FirstNameLookup(employee.FirstName);
            var searchRole = RoleLookup(employee.Role);

            return Close(
                        sessionAttributes,
                        "Fulfilled",
                        new LexResponse.LexMessage
                        {
                            ContentType = MESSAGE_CONTENT_TYPE,
                            Content = String.Format("Here is your search result for Employee with Last name {0}:  \n {1}\n", employee.LastName, searchLastName)
                            //Content = String.Format("Here is your search result for Employee with First name {0}:  \n {1}\n", employee.FirstName, searchFirstName)
                            //Content = String.Format("Here is your search result for Employee with role {0}:  \n {1}\n", employee.Role, searchRole)

                        }


                    );
            

            
        }
        

        private EmployeeLookUp CreateEmployeeLookUp(IDictionary<string, string> slots)
        {


            EmployeeLookUp employee = new EmployeeLookUp
            {

                LastName = slots.ContainsKey(LAST_NAME_SLOT) ? slots[LAST_NAME_SLOT] : null, 
                FirstName = slots.ContainsKey(FIRST_NAME_SLOT) ? slots[FIRST_NAME_SLOT] : null,
                Role = slots.ContainsKey(ROLE_SLOT) ? slots[ROLE_SLOT] : null,
                
            };

            return employee;
        }

        /// <summary>
        /// Verifies that any values for slots in the intent are valid.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private ValidationResult Validate(EmployeeLookUp employee)
        {

            if (!string.IsNullOrEmpty(employee.LastName))
            {

            }
            if (!string.IsNullOrEmpty(employee.FirstName))
            {

            }
            if (!string.IsNullOrEmpty(employee.Role))
            {

            }

            return ValidationResult.VALID_RESULT;
        }
        private string LastNameLookup(string lastName)
        {
            return JsonSerializer.Serialize(new DirectoryDataService().GetEmployees(2, lastName));
        }
        private string FirstNameLookup(string firstName)
        {
            return JsonSerializer.Serialize(new DirectoryDataService().GetEmployees(1, firstName));
        }
        private string RoleLookup(string role)
        {
            return JsonSerializer.Serialize(new DirectoryDataService().GetEmployees(3, role));
        }

    }


}


