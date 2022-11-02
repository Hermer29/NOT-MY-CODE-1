using System;
using System.Collections.Generic;

public class ActualToPerckForTravel
{
    public Action<Contract, List<Trailer>> EventStartToTravel;
    IRewardTrawel _rewardTravel;
    public ActualToPerckForTravel()
    {
        _rewardTravel = new AcrualRewardToTravel();
        EventStartToTravel += StartToTravel;
    }
    private void StartToTravel(Contract contract, List<Trailer> trailers)
    {
        _rewardTravel.Reward(contract, trailers);
    }
    public float ReturnReward()
    {
        return _rewardTravel.FinalReward;
    }
}