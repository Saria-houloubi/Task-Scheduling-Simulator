using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// The Design time view model for the instruction item with status
    /// </summary>
    public class FunctionalUnitWithStatusItemDesign : FunctionalUnitWithStatusModel
    {
        /// <summary>
        /// The inctance that will bind to for Design data
        /// </summary>
        public static FunctionalUnitWithStatusItemDesign Instance => new FunctionalUnitWithStatusItemDesign();

        public FunctionalUnitWithStatusItemDesign()
        {
            this.ID = 1;
            this.Name = "Add";
            this.IsBusy = false;
            this.Time = null;
            this.Operation = "SUB";
            this.TargetRegistery = "R8";
            this.SourceRegistery01= "R2";
            this.SourceRegistery02 = "R1";
            this.WaitingOperationForSorce01 = null;
            this.WaitingOperationForSorce02 = null;
            this.IsSorce01Ready = null;
            this.IsSorce02Ready=  null;
        }
    }
}
