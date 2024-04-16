using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DifficultyCard // Monobehaviour removed
{
    public string Title { get; set; }

    public string Info { get; set; }

    public abstract void ApplyDifficulty();
}

public class DetectRangeCard : DifficultyCard
{
    public DetectRangeCard()
    {
        Title = "Magnifying Glasses";
        Info = "Increases enemies detection range";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.enemyDetectRange *= 1.25f;
    }
}

public class DetectAngleCard : DifficultyCard
{
    public DetectAngleCard()
    {
        Title = "Wide Angle Lenses";
        Info = "Increases enemies detection field of view";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.enemyFOV += 45f;
    }
}

public class DetectTimeCard : DifficultyCard
{
    public DetectTimeCard()
    {
        Title = "Attention to Detail";
        Info = "Enemies detect you faster";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.detectingSpeed += StatsManager.Instance.detectingSpeed / 2;
    }
}

public class SpeedCard : DifficultyCard
{
    public SpeedCard()
    {
        Title = "New Shoes";
        Info = "Increases enemy movement speed";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.enemySpeed += 1.5f;

        if(StatsManager.Instance.enemyWaitSpeed > 0.25f)
            StatsManager.Instance.enemyWaitSpeed -= 0.25f;
    }
}

public class QuotaCard : DifficultyCard
{
    public QuotaCard()
    {
        Title = "I Need Gold";
        Info = "Quota increases by one";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.totalQuota += 1;
    }
}

