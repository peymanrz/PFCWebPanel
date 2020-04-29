using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PFCWebPanel.Interfaces;
using PFCWebPanel.Services;

namespace PFCWebPanel.Controllers
{
    public class NinjectController : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        //asteriskcdrdbEntities CdrDb = new asteriskcdrdbEntities();
        //asteriskEntities db = new asteriskEntities();
        public NinjectController()
        {
            ninjectKernel = new StandardKernel();
            AddBinding();
        }
        void AddBinding()
        {
            ninjectKernel.Bind<IAdmin>().To<AdminService>();
            ninjectKernel.Bind<IUser>().To<UserService>();
            //ninjectKernel.Bind<IManageUser>().To<ManageUser>();
            //ninjectKernel.Bind<IExtensionRepository>().To<ExtensionRepository>();
            //ninjectKernel.Bind<IQueueRepository>().To<QueueRepository>();
            //ninjectKernel.Bind<ICallCenterReports>().To<CallCenterReports>();
            ////ninjectKernel.Bind<IRecordingsRepository>().To<RecordingsRepository>();
            //ninjectKernel.Bind<IIvrRepository>().To<IvrRepository>();
            //ninjectKernel.Bind<IRecordAnouncmentRepository>().To<RecordAnouncmentRepository>();
            //ninjectKernel.Bind<IConferenceRepository>().To<ConferenceRepository>();
            //ninjectKernel.Bind<IOperatorsPollRepository>().To<OperatorsPollRepository>();


        }



        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

    }
}