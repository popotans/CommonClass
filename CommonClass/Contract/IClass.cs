using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonClass.Contract
{

    public interface ISite
    {
        int IDX { get; set; }
        string Title { get; set; }
    }

    public interface IChannel
    {
        int IDX { get; set; }
        string Title { get; set; }
        ISite Site { get; set; }
    }

    public interface IClass : ICloneable
    {
        int IDX { get; set; }
        string Title { get; set; }
        string Entitle { get; set; }
        ISite Site { get; set; }
        IChannel Channel { get; set; }
        int SiteId { get; set; }
        int ChannelId { get; set; }
        int Enable { get; set; }
        DateTime UpdateTime { get; set; }
        int OrderIndex { get; set; }



        List<IClass> GetAll(int siteid);
        List<IClass> GetAll(int siteid, int channelid);
        IClass GetByIdx(int classidx);
        IClass GetById(int classid, int depth);
        List<int> Navgination { get; set; }
        List<int> Childs { get; set; }
    }

    /*********
     * IDX       NAME       ID1        ID2         ID3          ID4            ID5          SITEID          CHANNELID       ENNAME      ORDERIDX    ENABLE  UpdateTime  
     * 1        新闻        1           0           0           0               0
     * 2        NAME        2           0           0           0               0
     * 3        国际新闻    1           1           0           0               0
     * 4        国内新闻    1           2           0
     * 5        NAME        1           1           1
     * 6        NAME        1           2           1
     * 7        NAME        1           2           2
     * 8        NAME        2           1           0
     * 9        NAME        2           1           1
     * 10       NAME        2           1           2
     * 11       NAME        2           2           0
     * 12       NAME        2           2           1
     * 13       NAME        2           2           2
     * 
     * 
     * 
     * ******/


}