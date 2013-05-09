using System;
using Rabbitus.Actors;
using Rabbitus.Context;

namespace PubSubTest
{
    public class SampleActor : Actor<SampleActor>
    {
        static SampleActor()
        {
            ForMessage<SampleMessage>()
                .HandledBy((actor, context) => actor.HandleSampleMessage(context));
        }
        
        public void HandleSampleMessage(IMessageContext<SampleMessage> context)
        {
            Console.WriteLine(context.Message.Timestamp);
        }
    }
}