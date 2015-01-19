using System;
using AutoMapper;
using NLog;
using rampsnamp.Core;
using rampsnamp.Web.Hubs;
using rampsnamp.Web.WindsorSetup;
using Raven.Abstractions.Data;
using Raven.Client;

namespace rampsnamp.Web.RavenDBObservers
{
    public class UsersObserver : IObserver<DocumentChangeNotification>
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DocumentChangeNotification documentChangeNotification)
        {
            if (documentChangeNotification.Type == DocumentChangeTypes.Put)
            {
                try
                {
                    using (var session = WindsorServiceLocator.Resolve<IDocumentStore>().OpenSession())
                    {
                        var userDto = Mapper.Map<User, UserDto>(session.Load<User>(documentChangeNotification.Id));

                        new UserHub().Send(userDto.Firstname);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("Hmm..");
                }
            }
        }
    }
}