using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.LexEvents;

namespace Chatbot1
{
    /// <summary>
    /// Base class for intent processors.
    /// </summary>
    public abstract class AbstractIntentProcessor : IIntentProcessor
    {

        internal const string MESSAGE_CONTENT_TYPE = "PlainText";

        /// <summary>
        /// Main method for proccessing the lex event for the intent.
        /// </summary>
        /// <param name="lexEvent"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public abstract LexResponse Process(LexEvent lexEvent, ILambdaContext context);

        protected string SerializeReservation(EmployeeLookUp order)
        {
            return JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
        }

        protected EmployeeLookUp DeserializeReservation(string json)
        {
            return JsonSerializer.Deserialize<EmployeeLookUp>(json);
        }

        protected LexResponse Close(IDictionary<string, string> sessionAttributes, string fulfillmentState, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Close",
                    FulfillmentState = fulfillmentState,
                    Message = message
                }
            };
        }
    
      
        protected LexResponse Close(IDictionary<string, string> sessionAttributes, string fulfillmentState, LexResponse.LexMessage message, LexResponse.LexMessage message1)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Close",
                    FulfillmentState = fulfillmentState,
                    Message = message1,
                    
           
                }
            };
        }

       

        protected LexResponse Delegate(IDictionary<string, string> sessionAttributes, IDictionary<string, string> slots)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "Delegate",
                    Slots = slots
                }
            };
        }
        

        protected LexResponse ElicitSlot(IDictionary<string, string> sessionAttributes, string intentName, IDictionary<string, string> slots, string slotToElicit, LexResponse.LexMessage message, LexResponse.LexResponseCard responseCard)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ElicitSlot",
                    IntentName = intentName,
                    Slots = slots,
                    SlotToElicit = slotToElicit,
                    Message = message,
                    ResponseCard = responseCard

                }

            };
        }
        protected LexResponse ElicitSlot(IDictionary<string, string> sessionAttributes, string intentName, IDictionary<string, string> slots, string slotToElicit, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ElicitSlot",
                    IntentName = intentName,
                    Slots = slots,
                    SlotToElicit = slotToElicit,
                    Message = message,
                  
                    

                }

            };
        }

        protected LexResponse ConfirmIntent(IDictionary<string, string> sessionAttributes, string intentName, IDictionary<string, string> slots, LexResponse.LexMessage message)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ConfirmIntent",
                    IntentName = intentName,
                    Slots = slots,
                    Message = message
                    
                }
            };
        }

        protected LexResponse ElicitIntent(IDictionary<string, string> sessionAttributes, string intentName, LexResponse.LexMessage message, LexResponse.LexResponseCard responseCard)
        {
            return new LexResponse
            {
                SessionAttributes = sessionAttributes,
                DialogAction = new LexResponse.LexDialogAction
                {
                    Type = "ElicitIntent",
                    IntentName = intentName,
                    ResponseCard = responseCard,
                    Message = message

                }
            };
        }


    }
}
