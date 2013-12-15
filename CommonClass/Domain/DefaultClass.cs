using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass.Contract;
namespace CommonClass.Domain
{
    /// <summary>
    /// from access db
    /// </summary>
    public class DefaultClass : BaseClass
    {
        public override List<IClass> GetAll(int siteid)
        {
            List<IClass> list = new List<IClass>();
            for (int i = 0; i < 3; i++)
            {
                IClass cls = new DefaultClass();
                cls.Title = "test" + i;
                list.Add(cls);
            }
            return list;
        }

        public override List<IClass> GetAll(int siteid, int channelid)
        {
            throw new NotImplementedException();
        }

        public override IClass GetByIdx(int classidx)
        {
            throw new NotImplementedException();
        }

        public override IClass GetById(int classid, int depth)
        {
            throw new NotImplementedException();
        }
    }
}