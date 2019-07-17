using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityData.Common
{
    public class ReturnResult
    {
        private Dictionary<string, object> _results = new Dictionary<string, object>();
        public string Message { get; set; }
        public string Code { get; set; }
        public bool HasData => _results.Count > 0 ? true : false;
        public bool HasError => !string.IsNullOrEmpty(Code) ? true : false;

        public void SetResult(string key, object result)
        {
            _results.Add(key, result);
        }

        public void SetResult<T>(T result) where T : class
        {
            _results.Add(typeof(T).ToString(), result);
        }

        public T GetResult<T>(string key) where T : class
        {
            return (T)_results[key];
        }

        public T GetResult<T>() where T : class
        {
            return (T)_results[typeof(T).ToString()];
        }

        public void AddMessage(string message)
        {
            Message = string.IsNullOrEmpty(Message) ? message : Message + "/-Next message-/" + message;
        }
    }

    public class ReturnResult2
    {
        private IteratorFactory _iteratorFactory;
        public string Message { get; set; }
        public string Code { get; set; }
        public bool HasData => _iteratorFactory.Count > 0 ? true : false;
        public bool HasError => !string.IsNullOrEmpty(Code) ? true : false;

        public ReturnResult2()
        {
            _iteratorFactory = new IteratorFactory();
        }

        public void SetResult<T>(T result) where T : class
        {
            _iteratorFactory.GetIterator<T>(result);
        }

        public T GetResult<T>() where T : class
        {
            return _iteratorFactory.GetIterator<T>().Take();
        }

        public void AddMessage(string message)
        {
            Message = string.IsNullOrEmpty(Message) ? message : Message + "/-Next message-/" + message;
        }
    }

    public class ReturnResult<T>
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public List<T> ListItem { get; set; }
        public T Item { get; set; }

        public bool HasData
        {
            get
            {
                return ListItem != null || Item != null ? true : false;
            }
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(Code) && Code != "0" ? true : false;
            }
        }

        public int TotalRow { set; get; }

        public void AddMessage(string message)
        {
            Message = string.IsNullOrEmpty(Message) ? message : Message + "/-Next message-/" + message;
        }
    }

    interface Iterator<T> where T : class
    {
        T First();
        T Next();
        T Take();
        T Current();
        bool IsDone();
    }

    class IteratorFactory
    {
        private Dictionary<string, object> _iterators = new Dictionary<string, object>();

        public int Count => _iterators.Count;

        public ConcreteIterator<T> GetIterator<T>(T item) where T : class
        {
            var key = typeof(T).ToString();
            if (!_iterators.ContainsKey(key)) _iterators.Add(key, new ConcreteIterator<T>());
            return ((ConcreteIterator<T>)_iterators[key]).Set(item);
        }

        public ConcreteIterator<T> GetIterator<T>() where T : class
        {
            var key = typeof(T).ToString();
            return ((ConcreteIterator<T>)_iterators[key]);
        }
    }

    class ConcreteIterator<T> : Iterator<T> where T : class
    {
        private List<T> _items;
        private int _current = 0;
        private bool _begin = true;

        public ConcreteIterator(List<T> items)
        {
            _begin = true;
            _items = items;
        }

        public ConcreteIterator()
        {
            _begin = true;
            _items = new List<T>();
        }

        public ConcreteIterator<T> Set(T item)
        {
            _items.Add(item);
            return this;
        }

        public T First()
        {
            _begin = false;
            return _items[0];
        }

        public T Next()
        {
            T ret = _items[_current];
            if (_current < _items.Count - 1)
            {
                ret = _items[++_current];
            }

            return ret;
        }

        public T Current()
        {
            return _items[_current];
        }

        public T Take()
        {
            if (_begin) return First();
            else return Next();
        }

        public bool IsDone()
        {
            return _current >= _items.Count;
        }
    }
}
