using System.Collections.Generic;

namespace Mini_Social_Media.Services
{
    public interface IDataService<T>
    {
        List<T> Records { get; set; }

        public void InitData();

        public void Add(T record)
        {
            if (record != null)
            {
                Records.Add(record);
            }
        }

        public void Remove(T record)
        {
            if (record != null)
            {
                Records.Remove(record);
            }
        }

        public T Find(string id);
        
        public bool CheckIfIdIsNotTaken(string id)
        {
            return Find(id) == null;
        }

        public bool CheckIfIdExists(string id)
        {
            return Find(id) != null;
        }
    }
}