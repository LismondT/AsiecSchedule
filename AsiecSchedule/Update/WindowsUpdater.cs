

namespace AsiecSchedule.Update
{
    internal class WindowsUpdater : IUpdater
    {
        public bool CheckPermissions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DownloadSource(string url, string path)
        {
            throw new NotImplementedException();
        }

        public void FreeTempResources(string sourcePath)
        {
            throw new NotImplementedException();
        }

        public void Update(string sourcePath)
        {
            throw new NotImplementedException();
        }
    }
}
