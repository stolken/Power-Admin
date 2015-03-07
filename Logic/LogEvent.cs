using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerAdmin.Models;

namespace PowerAdmin.Logic
{
    public static class LogEvent
    {
        public static void AddEvent(int ObjectID, string ObjectType,string MessageType, string Message, string Username )
        {
            var myEvent = new Event();
            myEvent.MessageType = MessageType;
            myEvent.Message = Message;
            myEvent.datetime = DateTime.Now;
            myEvent.Username = Username;
            myEvent.ObjectID = ObjectID;
            myEvent.ObjectType = ObjectType;

            // Get DB context.
            ScriptContext _db = new ScriptContext();

            // Add Script to DB.
            _db.Events.Add(myEvent);
            _db.SaveChanges();

            
        }

    }
}