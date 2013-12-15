using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonClass.Contract;

namespace CommonClass.Domain
{
    public abstract class BaseClass : IClass
    {
        public List<int> Navgination
        {
            get;
            set;
        }

        public List<int> Childs
        {
            get;
            set;
        }

        public int IDX
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Entitle
        {
            get;
            set;
        }

        public ISite Site
        {
            get;
            set;
        }

        public IChannel Channel
        {
            get;
            set;
        }

        public int SiteId
        {
            get;
            set;
        }

        public int ChannelId
        {
            get;
            set;
        }

        public int Enable
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }

        public int OrderIndex
        {
            get;
            set;
        }


        public abstract List<IClass> GetAll(int siteid)
       ;

        public abstract List<IClass> GetAll(int siteid, int channelid)
        ;

        public abstract IClass GetByIdx(int classidx)
        ;

        public abstract IClass GetById(int classid, int depth)
        ;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}