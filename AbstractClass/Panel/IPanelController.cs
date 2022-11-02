namespace AbstractClass.Panel
{
    public interface IPanelController
    {
        public void ResetValuePlayer();
        public void StartSwitchingInfoCard(bool isCurrentSwitch);
        public void OpenClosePanel(bool isSwitching);
        public PanelReplenishmentOfResources PanelReplenishmentOfResources { get; set; }
        public void SetState();
    }
}