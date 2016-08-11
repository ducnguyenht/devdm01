using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.SystemModule;

namespace RanchManagement.Module.Web.Controllers.Common
{
    public interface IModelWarnForUnsavedChanges
    {
        [Category("Behavior")]
        [DefaultValue(true)]
        bool WarnForUnsavedChanges { get; set; }
    }
    [ModelInterfaceImplementor(typeof(IModelWarnForUnsavedChanges), "Options")]
    public interface IModelClassWarnForUnsavedChanges : IModelWarnForUnsavedChanges
    {
        [Browsable(false)]
        [ModelValueCalculator("Application.Options")]
        IModelOptions Options { get; }
    }
    [ModelInterfaceImplementor(typeof(IModelWarnForUnsavedChanges), "ModelClass")]
    public interface IModelDetailViewWarnForUnsavedChanges : IModelWarnForUnsavedChanges
    {
    }

    [System.ComponentModel.DesignerCategory("Code")]
    public class UnsavedObjectController : ViewController<DetailView>, IModelExtender
    {
        private Boolean CanExitEditMode;
        private Boolean CancelActionTriggered;
        private Boolean SaveAndNewTriggered;
        private Boolean NewActionTriggered;
        private Boolean ExitEditModeByCancel;
        private Boolean IsObjectSpaceModified;
        private Boolean IsWarningShown;
        private String ActionActiveID = "ActionActiveReason";

        public UnsavedObjectController()
        {
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (Frame is DevExpress.ExpressApp.Web.PopupWindow || !View.IsRoot) return;

            if (((IModelDetailViewWarnForUnsavedChanges)View.Model).WarnForUnsavedChanges)
            {
                CanExitEditMode = false;
                CancelActionTriggered = false;
                SaveAndNewTriggered = false;
                NewActionTriggered = false;
                IsObjectSpaceModified = false;
                ExitEditModeByCancel = false;
                IsWarningShown = false;

                AdjustUIForMode(View.ViewEditMode);

                View.ViewEditModeChanged += View_ViewEditModeChanged;
                View.QueryCanClose += View_QueryCanClose;
                View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
                View.ObjectSpace.ObjectSaved += ObjectSpace_ObjectSaved;
            }
        }

        void ObjectSpace_ObjectSaved(object sender, ObjectManipulatingEventArgs e)
        {
            IsObjectSpaceModified = false;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            if (Frame is DevExpress.ExpressApp.Web.PopupWindow || !View.IsRoot) return;

            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            View.QueryCanClose -= View_QueryCanClose;
            View.ViewEditModeChanged -= View_ViewEditModeChanged;

            AdjustUIForMode(ViewEditMode.View);
        }

        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (!IsObjectSpaceModified) IsObjectSpaceModified = (e.OldValue != e.NewValue) | (e.Object != View.CurrentObject);
        }



        void View_ViewEditModeChanged(object sender, EventArgs e)
        {
            AdjustUIForMode(View.ViewEditMode);
        }

        void View_QueryCanClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !IsExitEditModeAllowed();
        }

        void HandleDetailActions(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ActionBase anAction = sender as ActionBase;
            if (anAction.Id == "Cancel")
            {
                CancelActionTriggered = !CancelActionTriggered;
                CanExitEditMode = !CancelActionTriggered;
                ExitEditModeByCancel = true;
                e.Cancel = !IsExitEditModeAllowed();
            }
            else if (anAction.Id == "New")
            {
                NewActionTriggered = !SaveAndNewTriggered;
                CanExitEditMode = !NewActionTriggered;
                SaveAndNewTriggered = false;
                e.Cancel = !IsExitEditModeAllowed();
            }
            else if (anAction.Id == "SaveAndNew")
            {
                SaveAndNewTriggered = true;
            }
            else
            {
                SaveAndNewTriggered = false;
                CancelActionTriggered = false;
                NewActionTriggered = false;
                CanExitEditMode = true;
                ExitEditModeByCancel = false;
            }
        }

        protected Boolean IsExitEditModeAllowed()
        {
            if ((View.ViewEditMode == ViewEditMode.Edit) && !CanExitEditMode && IsObjectSpaceModified && !(IsWarningShown && ExitEditModeByCancel))
            {
                String UnsavedObjectWarning;
                if (ExitEditModeByCancel) UnsavedObjectWarning = CaptionHelper.GetLocalizedText("Messages", "CancelAgainToCancelUnsavedChanges");
                else UnsavedObjectWarning = CaptionHelper.GetLocalizedText("Messages", "UnsavedChangesNotification");
                DevExpress.ExpressApp.Web.ErrorHandling.Instance.SetPageError(new UserFriendlyException(UnsavedObjectWarning));
                IsWarningShown = true;
                return false;
            }
            else return true;
        }

        protected void AdjustUIForMode(ViewEditMode EditMode)
        {
            if (EditMode == ViewEditMode.Edit)
            {
                Frame.GetController<WebModificationsController>().CancelAction.Executing += HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAction.Executing += HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAndCloseAction.Executing += HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAndNewAction.Executing += HandleDetailActions;
                Frame.GetController<WebNewObjectViewController>().NewObjectAction.Executing += HandleDetailActions;
            }
            else
            {
                Frame.GetController<WebModificationsController>().CancelAction.Executing -= HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAction.Executing -= HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAndCloseAction.Executing -= HandleDetailActions;
                Frame.GetController<WebModificationsController>().SaveAndNewAction.Executing -= HandleDetailActions;
                Frame.GetController<WebNewObjectViewController>().NewObjectAction.Executing -= HandleDetailActions;
            }

            Frame.GetController<WebRecordsNavigationController>().Active[ActionActiveID] = EditMode == ViewEditMode.View;
            Frame.GetController<RefreshController>().Active[ActionActiveID] = EditMode == ViewEditMode.View;
            // don't inactivate this because then save and new is deactivated also; instead, handle the click action of the new like the CancelAction
            // Frame.GetController<WebNewObjectViewController>().Active[ActionActiveID] = EditMode == ViewEditMode.View;
            Frame.GetController<WebDeleteObjectsViewController>().Active[ActionActiveID] = EditMode == ViewEditMode.View;
            // Frame.GetController<CloneObjectViewController>().Active[ActionActiveID] = EditMode == ViewEditMode.View;
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelOptions, IModelWarnForUnsavedChanges>();
            extenders.Add<IModelClass, IModelClassWarnForUnsavedChanges>();
            extenders.Add<IModelDetailView, IModelDetailViewWarnForUnsavedChanges>();
        }
    }
}
