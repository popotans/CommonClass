using System;
namespace CommonClass
{
    interface IBaseClass
    {
        CommonClass.IDb db { get;  set; }
        CommonClass.ClassInfo Get(int idx);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByIds(int[] idsArr);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP1(int p1);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP1All(int p1);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP2(int p2);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetRoot(int siteid);
        int Insert(CommonClass.ClassInfo ci);
        int InsertP1(CommonClass.ClassInfo ci);
        int InsertP1P2(CommonClass.ClassInfo ci);
        int InsertRoot(CommonClass.ClassInfo ci);
        int Update(CommonClass.ClassInfo ci);
    }
}
