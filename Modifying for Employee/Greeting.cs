using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Chatbot1
{
    class Greeting
    {
        public UserChoices? Choices { get; set; } // different BotOptions - Holidays, PTO, Benefits




        [JsonIgnore]
        public bool HasRequiredFlowerFields
        {
            get
            {
                return !string.IsNullOrEmpty(Choices.ToString());


            }
        }



        public enum UserChoices
        {
            Holidays,
            PTO,
            Benefits,
            Null
        }

    }
}

