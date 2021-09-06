using System.Collections;

using UnityEngine;

using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GroundCollider ground;

    private int score = 0;

    private Player player;
    private UIManager uiManager;

    private ISpawner spawner;

    [Inject]
    public void Construct(Player player, UIManager uiManager, ISpawner spawner)
    {
        this.player = player;
        this.uiManager = uiManager;

        this.spawner = spawner;


        player.OnDead += EndGame;
        player.OnScoreChanged += ScoreChanged;

        uiManager.onPlay += StartGame;
        uiManager.onPause += PauseGame;

        uiManager.onWindowPauseClosed += ContinueGame;
    }

    private void StartGame()
    {
        player.Go();
        spawner.StartSpawn();
    }
    private void ContinueGame()
    {
        spawner.Continue();
        ground.EnableAnimator(true);
        player.Input.UnBlockUnFreeze();
    }
    private void PauseGame()
    {
        player.Input.Freeze();
        spawner.Pause();
        ground.EnableAnimator(false);
    }
    private void EndGame()
    {
        spawner.StopSpawn();
        ground.EnableAnimator(false);

        StartCoroutine(UpdateScore());
    }

    private IEnumerator UpdateScore()
    {
        WindowScore windowScore = uiManager.WindowGameOver.WindowScore;

        int lastscoreBest = DataManager.ScoreBest;
        windowScore.SetBestScore(lastscoreBest.ToString());
        
        if (lastscoreBest < score)
        {
            DataManager.ScoreBest = score;
        }

        windowScore.SetScore("0");

        uiManager.ShowGameOver();

        yield return new WaitForSeconds(0.5f);


        for (int i = 1; i <= score; i++)
        {
            windowScore.SetScore(i.ToString());

            if (lastscoreBest < i)
            {
                windowScore.SetBestScore(i.ToString());
            }

            yield return new WaitForSeconds(0.2f);
        }

    }


    private void ScoreChanged(int value)
    {
        score += value;

        uiManager.SetScore(score.ToString());
    }
}