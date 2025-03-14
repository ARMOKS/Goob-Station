using Content.Shared._Goobstation.Heretic.Components;
using Content.Shared._Goobstation.Wizard;
using Content.Shared.Inventory.Events;

namespace Content.Shared.Heretic.Systems;

public sealed class HereticClothingSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HereticClothingComponent, BeingEquippedAttemptEvent>(OnEquipAttempt);
    }

    private void OnEquipAttempt(Entity<HereticClothingComponent> ent, ref BeingEquippedAttemptEvent args)
    {
        if (IsTargetValid(args.EquipTarget) && (args.EquipTarget == args.Equipee || IsTargetValid(args.Equipee)))
            return;

        args.Cancel();
        args.Reason = Loc.GetString("heretic-clothing-component-fail");
    }

    private bool IsTargetValid(EntityUid target)
    {
        return HasComp<HereticComponent>(target) || HasComp<GhoulComponent>(target) ||
               HasComp<WizardComponent>(target) || HasComp<ApprenticeComponent>(target);
    }
}
