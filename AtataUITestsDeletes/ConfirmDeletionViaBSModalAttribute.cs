using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atata;
using Atata.Bootstrap;

namespace AtataUITestsDeletes.ConfirmationPopups;

public class ConfirmDeletionViaBSModalAttribute : TriggerAttribute
{
    public ConfirmDeletionViaBSModalAttribute(TriggerEvents on = TriggerEvents.AfterClick, TriggerPriority priority = TriggerPriority.Medium)
        : base(on, priority)
    {
    }

    protected override void Execute<TOwner>(TriggerContext<TOwner> context) =>
        Go.To<DeletionConfirmationBSModal<TOwner>>(temporarily: true)
            .Delete();
}