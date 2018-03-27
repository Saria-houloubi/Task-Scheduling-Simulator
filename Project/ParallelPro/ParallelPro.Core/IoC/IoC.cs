using Ninject;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The IoC class that will work with to get data accross the hole application
    /// </summary>
    public static class IoC
    {
        #region Public Properties
        /// <summary>
        /// The kernel that will bind all the classes to
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();
        #endregion

        #region Short cuts

        /// <summary>
        /// A short cut for the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Appliation => IoC.Kernel.Get<ApplicationViewModel>();
        /// <summary>
        /// A short cut for the ui
        /// </summary>
        public static IUIManager UI => IoC.Kernel.Get<IUIManager>();
        /// <summary>
        /// short cut to get the exam results
        /// </summary>
        public static StudentExamInformationAndMarksViewModel ExamInfo => IoC.Kernel.Get<StudentExamInformationAndMarksViewModel>();
        #endregion

        #region Construction
        /// <summary>
        /// Sets the binding for all the classes
        /// </summary>
        public static void Setup()
        {
            BindViewModels();
        }
        /// <summary>
        /// Binds the viewmodels to the kernel so we can use them
        /// </summary>
        private static void BindViewModels()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
            Kernel.Bind<StudentExamInformationAndMarksViewModel>().ToConstant(new StudentExamInformationAndMarksViewModel());
        } 
        #endregion
    }
}
