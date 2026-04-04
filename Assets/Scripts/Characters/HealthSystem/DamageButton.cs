public class DamageButton : HealthButton
{
    protected override void Clicked()
    {
        Health.TakeDamage(Value);
    }
}