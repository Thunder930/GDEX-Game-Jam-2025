# GDEX-Game-Jam-2025 - Corruptor
Hello everyone! This is my submission for my very first game jam! I worked on it as a solo developer, so it's nothing too complex. Feel free to fork this if you feel you can improve upon it!

# Description
This game revolves around placing purification blocks to fight back a spreading corruption. You can solve each level by simply spamming purification blocks, but the challenge is to beat the corruption with the lowest amount of blocks in the lowest amount of time.

# Controls
- Left Mouse Button: Place selected block wherever your cursor is. You can only place blocks in empty spaces.
- Right Mouse Button: Remove the block where your cursor is. You can only remove blocks that you’ve placed.
- Scroll Wheel: Switch selected block. The game has two different blocks you can choose from, a Purification Block and a Timed Purification Block. The difference between the two is discussed in the ‘Blocks’ section.
- Tab: Pauses/Unpauses the game and opens/closes the pause menu.

# Blocks
- Corruptible Ground: The medium through which corruption spreads. Once a block of this type has been fully corrupted, it will start spreading the corruption to its neighbors. It will automatically purify if there aren’t any corrupting forces adjacent to it.

- Corrupting Node: Spreads the corruption to surrounding blocks. Unlike Corruptible Ground, it doesn’t auto purify when there isn’t any adjacent corruption and it can only be purified if all surrounding Corruptible Ground has been purified first. Once purified, it turns into an Inert Node and cannot be recorrupted.

- Purification Block: Your main tool for fighting back corruption. It provides a purifying power to all adjacent blocks, which in turn is passed along to the blocks adjacent to those up to a certain distance.

- Timed Purification Block: Acts a lot like the Purification Block, but its performance degrades with time.

# Notably Missing
- This game has no sound. The best I could've done is thrown together some beeps and boops in Bfxr, and designing a sound track was completely out of the question for me.

- Pipes were going to be in the game and have levels designed around them. They would’ve linked two blocks so that they acted as though they were adjacent. Unfortunately, the way I implemented them created an infinite loop that blew out the call stack, and I didn’t have the time or energy to figure out a better way of implementing them.

# Other Notes
- Level 4 might seem impossible, but I assure you it's not! Remember, you have two different blocks available to you.

- Level 5 actually can become impossible to solve if you aren’t fast enough. If you reach that point, you can always pull up the pause menu by pressing ‘Tab’ and hit ‘Restart’ to reset the level.

# Tools Used
- Game Engine: Unity
- Sprites: Paint.net
