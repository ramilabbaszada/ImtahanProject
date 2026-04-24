using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using ExamApplication.Business.Abstract;
using ExamApplication.Business.Concrete;
using ExamApplication.Business.Interceptors;
using ExamApplication.DataAccess.Abstract;
using ExamApplication.DataAccess.Concrete.EntityFramework;
using System.Reflection;
using Module = Autofac.Module;

namespace ExamApplication.Business.DependencyResolvers
{
    public class AutoFacBusinessModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();

            builder.RegisterType<ExamManager>().As<IExamService>().InstancePerLifetimeScope();
            builder.RegisterType<EfExamDal>().As<IExamDal>().InstancePerLifetimeScope();

            builder.RegisterType<StudentManager>().As<IStudentService>().InstancePerLifetimeScope();
            builder.RegisterType<EfStudentDal>().As<IStudentDal>().InstancePerLifetimeScope();

            builder.RegisterType<SubjectManager>().As<ISubjectService>().InstancePerLifetimeScope();
            builder.RegisterType<EfSubjectDal>().As<ISubjectDal>().InstancePerLifetimeScope();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
