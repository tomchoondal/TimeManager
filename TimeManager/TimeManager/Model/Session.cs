using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Model
{
    public class Session
    {
        public Session() { }

        public Session(DateTime dateTime)
        {
            TimeLine = new List<DateTime>();
            TimeLine.Add(dateTime);
            IsActive = true;
        }

        public bool IsActive { get; set; }

        public DateTime BeginTime
        {
            get
            {
                DateTime beginTime = DateTime.MinValue;
                if (IsActive && TimeLine != null && TimeLine.Count > 0)
                {
                    beginTime = TimeLine.FirstOrDefault();
                    if (beginTime == null)
                    {
                        beginTime = DateTime.MinValue;
                    }
                }
                return beginTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return (TimeLine != null && TimeLine.Count > 1) ? TimeLine.LastOrDefault() : DateTime.MinValue;
            }
        }

        public List<DateTime> TimeLine { get; set; }
    }
}
