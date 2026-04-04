public class HealButton : HealthButton
{
    protected override void Clicked()
    {
        Health.ExcuteHeal(Value);
    }
}