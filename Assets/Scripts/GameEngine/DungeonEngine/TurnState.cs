namespace Assets.GameEngine.DungeonEngine {
    // Might be too granular, but things like status or recoil happen between turns, so they might be neccessary. 
    public enum TurnState
    {
        PrePlayer,
        Player,
        PostPlayer,
        PreMovement,
        Movement,
        PostMovement,
        PreTeammates,
        Teammates,
        PostTeammates,
        PreEnemies,
        Enemies,
        PostEnemies
    }
}