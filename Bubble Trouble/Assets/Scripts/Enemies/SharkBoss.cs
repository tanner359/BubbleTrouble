
public class SharkBoss : Enemy
{
    private void OnDestroy()
    {
        //INSERT WORLD CLEAR ANIMATION/BANNER HERE

        PlayerData.UnlockWorld(2);
        Launcher.instance.LoadLevel(1);
    }

    ////Wait a few seconds before unlocking the world and loading back to the World Select 
    //public IEnumerator WorldCooldown()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}
