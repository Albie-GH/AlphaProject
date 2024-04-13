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
        Title = "Zoom Optics";
        Info = "Increases enemies detection range.";
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
        Title = "Wide Angle Lens";
        Info = "Increases enemies detection field of view.";
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
        Title = "Advanced Callibration";
        Info = "Decreases time allowed before detection.";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.enemyDetectTime /= 2;
    }
}

public class SpeedCard : DifficultyCard
{
    public SpeedCard()
    {
        Title = "Motor Upgrade";
        Info = "Increases enemy movement speed.";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.enemySpeed += 1.5f;
    }
}

public class QuotaCard : DifficultyCard
{
    public QuotaCard()
    {
        Title = "Quota Increase";
        Info = "Quota increases by one.";
    }

    public override void ApplyDifficulty()
    {
        StatsManager.Instance.totalQuota += 1;
    }
}

