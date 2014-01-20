using System;
using System.Collections.Generic;
using CommonClass;
namespace CommonClass
{
    public interface IBaseClass
    {
        CommonClass.IDb db { get; set; }
        CommonClass.ClassInfo Get(int idx);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByIds(int[] idsArr);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP1(int p1);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP1All(int p1);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetByP2(int p2);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetRoot(int siteid);
        System.Collections.Generic.List<CommonClass.ClassInfo> GetAll(int siteid);

        List<ClassInfo> GetRoot(List<ClassInfo> all);
        List<ClassInfo> GetByP2(List<ClassInfo> all, int p2);
        List<ClassInfo> GetByP1All(List<ClassInfo> all, int p1);
        List<ClassInfo> GetByP1(List<ClassInfo> all, int p1);


        int Insert(CommonClass.ClassInfo ci);
        int InsertP1(CommonClass.ClassInfo ci);
        int InsertP1P2(CommonClass.ClassInfo ci);
        int InsertRoot(CommonClass.ClassInfo ci);
        int Update(CommonClass.ClassInfo ci);


        void InitDropDownList(int siteid, System.Web.UI.WebControls.DropDownList ddl);
    }
}
