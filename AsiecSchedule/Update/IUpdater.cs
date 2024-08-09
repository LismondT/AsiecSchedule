namespace AsiecSchedule.Update
{
    public interface IUpdater
    {
        public bool CheckPermissions();
        public void Update(string sourcePath);
        public void FreeTempResources(string sourcePath);
    }
}