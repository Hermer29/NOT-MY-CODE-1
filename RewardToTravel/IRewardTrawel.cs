
using System.Collections.Generic;

public interface IRewardTrawel
{
    public void Reward(Contract contract, List<Trailer> Tailers);
    public float FinalReward { get; set; }
}

