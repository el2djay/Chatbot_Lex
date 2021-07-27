using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Chatbot1
{
    /// <summary>
    /// A utility class to store all the current values from the intent's slots.
    /// </summary>
    public class EmployeeLookUp
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }



        [JsonIgnore]
        public bool HasRequiredFlowerFields
        {
            get
            {
                return !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(Role);
                       
                       

            }
        }
       
    }
}
