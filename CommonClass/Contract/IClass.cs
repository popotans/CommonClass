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

    public interface IClassDao
    {
        int IDx { get; set; }
        int P1 { get; set; }
        int P2 { get; set; }
        string Title { get; set; }
        IDB db { get; set; }


        ClassInfo GetInfo(int idx);
        List<ClassInfo> GetRoot();
        List<ClassInfo> GetByP1(int p1);
        List<ClassInfo> GetByP1All(int p1);
        List<ClassInfo> GetByP2(int p2);


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