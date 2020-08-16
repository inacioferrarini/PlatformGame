using UnityEngine;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{

    /// <summary>
    /// Manages collisions between two different game objects.
    /// </summary>
    class CollisionManager
    {
        private GameManager mp_gameManager;

        public CollisionManager(GameManager p_gameManager)
        {
            mp_gameManager = p_gameManager;
        }

        public void HandleCollision(GameObject object1, GameObject object2)
        {
            if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.gem))
                || (object1.CompareTag(Constants.Collision.Tags.gem) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                CollectableItem item = object1.GetComponent<CollectableItem>();
                if (object2.CompareTag(Constants.Collision.Tags.gem))  // TODO: Change this tag to Item
                {
                    item = object2.GetComponent<CollectableItem>();
                }

                HandlePlayerGetGem(item);
            }

            if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.exit))   // TODO: Improve this . Create a method that returns an Enum.
                || (object1.CompareTag(Constants.Collision.Tags.exit) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                Player player = object1.GetComponent<Player>();
                if (object2.CompareTag(Constants.Collision.Tags.player))
                {
                    player = object2.GetComponent<Player>();
                }

                HandlePlayerExit(player);
            }

            if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.enemy))   // TODO: Improve this . Create a method that returns an Enum.
                || (object1.CompareTag(Constants.Collision.Tags.enemy) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                Player player = object1.GetComponent<Player>();
                if (object2.CompareTag(Constants.Collision.Tags.player))
                {
                    player = object2.GetComponent<Player>();
                }

                HandlePlayerDie(player);
            }
        }

        private void HandlePlayerGetGem(CollectableItem item)
        {
            mp_gameManager.mp_score += item.points;
            SoundManager.instance.PlayFxItem(item.collectFx);
            Object.Destroy(item.gameObject);
        }

        private void HandlePlayerExit(Player player)
        {
            player.LevelCompleted();
            SoundManager.instance.PlayFxItem(player.m_winFx);
        }

        private void HandlePlayerDie(Player player)
        {
            player.PlayerDie();
            Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy);
            SoundManager.instance.PlayFxPlayer(player.m_dieFx);
        }

    }

}