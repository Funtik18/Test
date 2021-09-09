using UnityEngine;

using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GroundCollider ground;

    private int score = 0;

    private IEntity entityPlayer;
    private ISpawner spawner;

    private UIManager uiManager;

    [Inject]
    public void Construct(IEntity entityPlayer, ISpawner spawner, UIManager uiManager)
    {
        this.entityPlayer = entityPlayer;
        this.spawner = spawner;

        this.uiManager = uiManager;


        entityPlayer.onDead += EndGame;
        entityPlayer.onScoreChanged += ScoreChanged;

        uiManager.onPlay += StartGame;
        uiManager.onPause += PauseGame;

        uiManager.onWindowPauseClosed += ContinueGame;
    }

    private void StartGame()
    {
        entityPlayer.Go();
        spawner.StartSpawn();
    }
    private void ContinueGame()
    {
        spawner.Continue();
        ground.EnableAnimator(true);
        entityPlayer.Input.UnBlockUnFreeze();
    }
    private void PauseGame()
    {
        entityPlayer.Input.Freeze();
        spawner.Pause();
        ground.EnableAnimator(false);
    }
    private void EndGame()
    {
        spawner.StopSpawn();
        ground.EnableAnimator(false);

        uiManager.TryUpdateBestScore(score);
    }

    private void ScoreChanged(int value)
    {
        score += value;

        uiManager.SetGameScore(score.ToString());
    }
}