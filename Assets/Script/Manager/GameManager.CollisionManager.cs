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

        /// <summary>
        /// Possible game status.
        /// </summary>
        public enum CollisionMembers
        {
            PLAYER_GEM, PLAYER_EXIT, PLAYER_ENEMY, NONE
        }

        public CollisionManager(GameManager p_gameManager)
        {
            mp_gameManager = p_gameManager;
        }

        public void HandleCollision(GameObject object1, GameObject object2)
        {
            CollisionMembers collisionMembers = EvaluateCollision(object1, object2);
            if (collisionMembers == CollisionMembers.PLAYER_GEM)
            {
                CollectableItem item = SafeGetCollectableItem(object1, object2);
                HandlePlayerGetGem(item);
            }
            else if (collisionMembers == CollisionMembers.PLAYER_EXIT)
            {
                Player player = SafeGetPlayer(object1, object2);
                HandlePlayerExit(player);
            }
            else if (collisionMembers == CollisionMembers.PLAYER_ENEMY)
            {
                Player player = SafeGetPlayer(object1, object2);
                HandlePlayerDie(player);
            }
        }

        private CollisionMembers EvaluateCollision(GameObject object1, GameObject object2)
        {
            if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.gem))
                || (object1.CompareTag(Constants.Collision.Tags.gem) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                return CollisionMembers.PLAYER_GEM;
            }
            else if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.exit))
                || (object1.CompareTag(Constants.Collision.Tags.exit) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                return CollisionMembers.PLAYER_EXIT;
            }
            else if ((object1.CompareTag(Constants.Collision.Tags.player) && object2.CompareTag(Constants.Collision.Tags.enemy))
                || (object1.CompareTag(Constants.Collision.Tags.enemy) && object2.CompareTag(Constants.Collision.Tags.player)))
            {
                return CollisionMembers.PLAYER_ENEMY;
            }
            return CollisionMembers.NONE;
        }

        private CollectableItem SafeGetCollectableItem(GameObject object1, GameObject object2)
        {
            CollectableItem item = object1.GetComponent<CollectableItem>();
            if (object2.CompareTag(Constants.Collision.Tags.gem))
            {
                item = object2.GetComponent<CollectableItem>();
            }
            return item;
        }

        private Player SafeGetPlayer(GameObject object1, GameObject object2)
        {
            Player player = object1.GetComponent<Player>();
            if (object2.CompareTag(Constants.Collision.Tags.player))
            {
                player = object2.GetComponent<Player>();
            }
            return player;
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