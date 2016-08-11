using System;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.Base;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridLookup;
using DevExpress.Web.ASPxGridView;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Web.SystemModule;

namespace RanchManagement.Module.Web.Editors
{
    [PropertyEditor(typeof(object), "CustomWebLookup", false)]
    public class ManagerAspxGridLookupPropertyEditor : ASPxObjectPropertyEditorBase
    {
        private WebLookupEditorHelper helper;
        ASPxGridLookup control = null;
        public ManagerAspxGridLookupPropertyEditor(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
        protected override WebControl CreateEditModeControlCore()
        {
            IModelListView modelListView = this.Model.View as IModelListView;
            if (modelListView == null)
            {
                modelListView = application.FindModelClass(MemberInfo.MemberTypeInfo.Type).DefaultLookupListView;
            }
            modelListView.UseServerMode = true;
            CollectionSource collectionSource = new CollectionSource(objectSpace, MemberInfo.MemberTypeInfo.Type, modelListView.UseServerMode);
            ListView tempListView = this.application.CreateListView(modelListView, collectionSource, false);
            tempListView.CreateControls();
            control = new ASPxGridLookup();
            control.KeyFieldName = MemberInfo.MemberTypeInfo.KeyMember.Name;
            control.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            control.IncrementalFilteringDelay = 800;
            control.GridView.AutoGenerateColumns = false;
            control.SelectionMode = GridLookupSelectionMode.Single;
            if (tempListView.Editor != null && tempListView.Editor is ASPxGridListEditor)
            {
                ASPxGridView tempGridView = ((ASPxGridListEditor)tempListView.Editor).Grid;

                foreach (GridViewColumn tempColumn in tempGridView.Columns)
                {
                    control.GridView.Columns.Add(tempColumn);
                }
                control.GridView.Settings.Assign(tempGridView.Settings);
                control.GridView.Settings.ShowHeaderFilterButton = false;
                control.GridView.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
                if (control.Columns.Count > 1)
                {
                    control.TextFormatString = "{1} | {0}";
                }
                control.GridView.SettingsPager.PageSize = 10;

                control.GridView.Width = 300;
                control.DataSource = collectionSource.Collection;
            }
            control.ValueChanged += EditValueChangedHandler;
            //control.ValueChanged += control_ValueChanged;
            return control;
        }

        protected override void SetImmediatePostDataScript(string script)
        {
            control.ClientSideEvents.ValueChanged = script;
            //findEdit.TextBox.ClientSideEvents.TextChanged = script;
        }
        protected override void SetImmediatePostDataCompanionScript(string script)
        {
            control.SetClientSideEventHandler("ValueChanged", script);
        }
        protected override void ReadEditModeValueCore()
        {
            BaseObject obj = this.CurrentObject as BaseObject;
            var propertyValue = obj.GetMemberValue(this.propertyName) as BaseObject;
            if (propertyValue != null)
            {
                control.GridView.Selection.SelectRowByKey(propertyValue.Oid);
            }
            if (MemberInfo.GetValue(CurrentObject) == null) { this.control.Text = ""; }
        }

        protected override object GetControlValueCore()
        {
            if (ViewEditMode == ViewEditMode.Edit && Editor != null)
            {
                var editor = this.Editor as ASPxGridLookup;
                if (editor.Value != null)
                {   //get selected Obj
                    return objectSpace.GetObjectByKey(MemberInfo.MemberType, editor.Value);
                }
                return null;
            }
            return MemberInfo.GetValue(CurrentObject);
        }
        protected override void WriteValueCore()
        {
            base.WriteValueCore();
        }
        public override void Setup(DevExpress.ExpressApp.IObjectSpace objectSpace, DevExpress.ExpressApp.XafApplication application)
        {
            base.Setup(objectSpace, application);
            helper = new WebLookupEditorHelper(application, objectSpace, MemberInfo.MemberTypeInfo, Model);
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            base.BreakLinksToControl(unwireEventsOnly);
        }
    }
}


//using System;
//using System.Web.Configuration;
//using System.Web.UI.WebControls;
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Model;
//using DevExpress.ExpressApp.SystemModule;
//using DevExpress.ExpressApp.Utils;
//using DevExpress.ExpressApp.Web;
//using DevExpress.ExpressApp.Web.Editors;
//using DevExpress.ExpressApp.Web.Editors.ASPx;
//using DevExpress.Persistent.Base;
//using DevExpress.Web.ASPxEditors;
//using DevExpress.Web.ASPxGridLookup;
//using DevExpress.Web.ASPxGridView;
//using DevExpress.Xpo;
//using DevExpress.ExpressApp.Xpo;
//using DevExpress.Persistent.BaseImpl;
//using DevExpress.ExpressApp.Web.SystemModule;

//namespace ASPxDropDownEdit.Module.Web.Editors
//{
//    [PropertyEditor(typeof(object), "CustomWebLookup", false)]
//    public class ManagerAspxGridLookupPropertyEditor : ASPxObjectPropertyEditorBase
//    {
//        private NestedFrame frame;
//        private WebLookupEditorHelper helper;
//        private object newObject;
//        private IObjectSpace newObjectSpace;
//        ASPxGridLookup control = null;
//        public ManagerAspxGridLookupPropertyEditor(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }   
//        protected void AddGridViewColumns(DevExpress.Web.ASPxGridLookup.ASPxGridLookup control, string MemberTypeInfo)
//        {
//            switch (MemberTypeInfo)
//            {
//                case "SinhVien":
//                     GridViewDataTextColumn colLastName = new GridViewDataTextColumn() { Caption = "Name Sv", FieldName = "Name", SortOrder = DevExpress.Data.ColumnSortOrder.Descending };
//                    control.Columns.Add(colLastName);
//                    GridViewDataTextColumn colFirstName = new GridViewDataTextColumn() { Caption = "Code Sv", FieldName = "Id" };
//                    control.Columns.Add(colFirstName);
                   
//                    break;
//                case "Lop":
//                    GridViewDataTextColumn bb = new GridViewDataTextColumn() { Caption = "Name Lop", FieldName = "Name", SortOrder = DevExpress.Data.ColumnSortOrder.Descending };
//                    control.Columns.Add(bb);
//                    GridViewDataTextColumn aa = new GridViewDataTextColumn() { Caption = "Code Lop", FieldName = "Id" };//, SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
//                    control.Columns.Add(aa);                 
//                    break;
//            }
//        }
      
//        protected override WebControl CreateEditModeControlCore()
//        {
//            if (frame == null)
//            {
//                frame = helper.Application.CreateNestedFrame(this, TemplateContext.LookupControl);
//                frame.SetView(helper.CreateListView(CurrentObject));
//            }
//            control = new ASPxGridLookup();  
//            XpoDataSource datasource = new XpoDataSource();
//            //datasource.Criteria = "IsGrainBosRecord = True";
//            datasource.TypeName = MemberInfo.MemberTypeInfo.FullName;
//            datasource.ServerMode = true;
//            datasource.Session = ((XPObjectSpace)objectSpace).Session;
//            control.DataSource = datasource;
//            control.GridView.SettingsPager.PageSize = 10;
//            //control.GridView.SettingsCookies.CookiesID = "CustomerGridID";
//            //control.GridView.SettingsCookies.Enabled = true;
//            //control.GridView.SettingsCookies.StoreFiltering = true;
//            //control.GridView.SettingsCookies.StorePaging = true;
//            //control.GridView.SettingsCookies.StoreColumnsVisiblePosition = true;
//            //control.GridView.SettingsCookies.StoreColumnsWidth = true;
//            //control.GridView.SettingsCookies.StoreGroupingAndSorting = true;
//            control.KeyFieldName = MemberInfo.MemberTypeInfo.KeyMember.Name;
//            control.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
//            control.TextFormatString = "{0}";
//            control.IncrementalFilteringDelay = 800;
//            control.ValueChanged += EditValueChangedHandler;
//            //control.GridView.AutoGenerateColumns = true;
//            control.GridView.AutoGenerateColumns = false;
//            control.GridView.Settings.ShowFilterRow = true;
//            control.GridView.Settings.ShowFilterRowMenu = true;
//            control.GridView.SettingsCookies.Enabled = true;
//            control.GridView.Width = 600;
//            control.SelectionMode = GridLookupSelectionMode.Single;
//            AddGridViewColumns(control, MemberInfo.MemberType.Name);
//            return control;
//        }
//        public bool flag = true;
//        protected override void ReadEditModeValueCore()
//        {
//            if (flag)
//            {   //load du lieu ban dau neu edit
//                var obj = this.View.CurrentObject as BaseObject;
//                control.Load += delegate(object s, EventArgs e)
//                {
//                    var propertyValue = obj.GetMemberValue(this.propertyName) as BaseObject;
//                    if (propertyValue != null)
//                    {
//                        control.GridView.Selection.SelectRowByKey(propertyValue.Oid);
//                    }
//                };
//            }
//            flag = false;
//            //reset field neu save and new
//            if (MemberInfo.GetValue(CurrentObject) == null) { this.control.Text = ""; }
//        }

//        protected override object GetControlValueCore()
//        {
//            if (ViewEditMode == ViewEditMode.Edit && Editor != null)
//            {
//                var editor = this.Editor as ASPxGridLookup;
//                if (editor.Value != null)
//                {   //get selected Obj
//                    return objectSpace.GetObjectByKey(MemberInfo.MemberType, editor.Value);
//                }
//                return null;
//            }
//            return MemberInfo.GetValue(CurrentObject);
//        }
//        public override void Setup(DevExpress.ExpressApp.IObjectSpace objectSpace, DevExpress.ExpressApp.XafApplication application)
//        {
//            base.Setup(objectSpace, application);
//            helper = new WebLookupEditorHelper(application, objectSpace, MemberInfo.MemberTypeInfo, Model);
//        }
//        public override void BreakLinksToControl(bool unwireEventsOnly)
//        {
//            base.BreakLinksToControl(unwireEventsOnly);
//        }
//    }
//}
