namespace AsiecSchedule.Update
{
    public interface IUpdater
    {
        public Task Update(string version);

        public void FreeTempResources();
    }
}