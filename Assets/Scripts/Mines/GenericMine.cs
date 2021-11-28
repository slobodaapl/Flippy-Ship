public class GenericMine : Impactable<MineSpawnable>
{
    public int GetDamage()
    {
        return collisionDamage;
    }

    public void EnactCollission()
    {
        spawnable.UpdateConstraints(gameObject);
        Destroy(gameObject);
    }
}