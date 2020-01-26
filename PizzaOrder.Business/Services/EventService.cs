using PizzaOrder.Business.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace PizzaOrder.Business.Services
{
    public interface IEventService
    {
        IObservable<EventDataModel> OnCreateObservable { get; }

        void CreateOrderEvent(EventDataModel orderEvent);
    }

    public class EventService : IEventService
    {
        private readonly ISubject<EventDataModel> _onCreateSubject;

        public EventService()
        {
            _onCreateSubject =  new ReplaySubject<EventDataModel>(1);
        }

        public void CreateOrderEvent(EventDataModel orderEvent) => _onCreateSubject.OnNext(orderEvent);

        public IObservable<EventDataModel> OnCreateObservable => _onCreateSubject.AsObservable(); 
      
    }
}
