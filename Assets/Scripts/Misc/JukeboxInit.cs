using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JukeboxInit : MonoBehaviour
{
    /* Define arrays for resources */
    public Image[] stickers; // For each pair, 0 is inactive frame and 1 is active frame
    public Image[] pointers; // easier than moving one pointer around
    public Image[] backgrounds;
    public AudioSource[] soundtrack;
    public string[] soundtrackTitles; // Gonna have to manually add all the songs ig

    public Text titleDisplay; // Displays the currently playing song name

    /* Internal global variables */
    int track; // current track that is playing
    int r; // random seed to display which sticker 
    int pointer; // location of menu pointer
    int bg; // index of current background

    bool songPlaying; // true if a song is being played
    bool songPlayed; // true if a song was played (and may now be paused)

    Coroutine stickerAnim; // coro for sticker animation

    bool loop; // if true, loop song, if false, go on

    public bool animateSticker;
    public bool loopBackground;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables to default values
        pointer = 0;
        track = 0;
        songPlaying = false;
        songPlayed = false;
        loop = false;
        titleDisplay.text = soundtrackTitles[0];
        bg = 0;

        // Hide all images until they are needed
        foreach (Image i in backgrounds) i.CrossFadeAlpha(0, 0.0001f, false); // probably not best way to do this
        foreach (Image i in stickers) i.enabled = false;
        foreach (Image i in pointers) i.enabled = false;

        r = new System.Random().Next(2) * 2;

        // TODO: Request more art from artists?
        backgrounds[bg].CrossFadeAlpha(1, 0.0001f, false); // Display first background
        stickers[r].enabled = true; // choose whether to display Chatot or Shaymin
        pointers[pointer].enabled = true; // Initialize pointer at first menu item


        // Start BG fade coro
        StartCoroutine(BackgroundFade());
        
    }

    // Update is called once per frame
    void Update()
    {
        // handle menuing
        if (Input.GetKeyDown("down"))
        {
            SetPointer((pointer + 1 + pointers.Length) % pointers.Length); // this should take care of wrapping
        }
        else if (Input.GetKeyDown("up"))
        {
            SetPointer((pointer - 1 + pointers.Length) % pointers.Length); // this should take care of wrapping
        }
        else if (Input.GetKeyDown("space")) // Select menu item - equivalent to onClick for that button
        {
            switch(pointer)
            {
                case 0:
                    Pause(); // called pause but really handles pause and play
                    break;
                case 1:
                    Stop(); // stop
                    break;
                case 2:
                    Stop();
                    DeltaTrack(1); // stop and play the next track
                    break;
                case 3:
                    Stop();
                    DeltaTrack(-1); // stop and play the previous track
                    break;
                case 4:
                    ToggleLoop(); // change state of loop
                    break;
                case 5:
                default:
                    Exit();
                    break;// back to main menu
            }
        }

        // Handle once song finishes
        if (songPlaying && !soundtrack[track].isPlaying)
        {
            // Play next song, or this song again
            if (loop)
            {
                DeltaTrack(0);
            }
            else
            {
                DeltaTrack(1);
            }
        }

    }

    /* Coroutines for animation */
    // TODO
    WaitForSeconds w_bgf = new WaitForSeconds(10.0f);

    IEnumerator BackgroundFade()
    {
        while(loopBackground)
        {
            backgrounds[bg].CrossFadeAlpha(0, 10.0f, false);
            bg = (bg + 1) % backgrounds.Length;
            backgrounds[bg].CrossFadeAlpha(1, 10.0f, false);
            yield return w_bgf;
        }
    }

    WaitForSeconds w_sa = new WaitForSeconds(2.0f);

    IEnumerator StickerAnimation()
    {
        while(animateSticker)
        {
            stickers[r].enabled = !stickers[r].enabled;
            stickers[r + 1].enabled = !stickers[r + 1].enabled;
            yield return w_sa;
        }
    }

    /* Helper method for changing the position of the pointer */
    void SetPointer(int point)
    {
        pointers[pointer].enabled = false;
        pointers[point].enabled = true;
        pointer = point;
    }

    /* Internal method for pause/play */
    void Pause()
    {
        if (songPlaying) // pause song if song is playing
        {
            soundtrack[track].Pause();
            songPlayed = true;
            songPlaying = false;
            StopCoroutine(stickerAnim);
        }
        else if (!songPlaying) // play song if song is paused
        {
            if (songPlayed)
            {
                soundtrack[track].UnPause();
            }
            else if (!songPlayed)
            {
                soundtrack[track].Play();
            }
            stickerAnim = StartCoroutine(StickerAnimation());
            songPlaying = true;
        }
    }

    /* Internal method for stopping a song */
    void Stop()
    {
        // reset values
        songPlaying = false;
        songPlayed = false;
        // put sticker back at static frame of animation
        stickers[r].enabled = true;
        stickers[r + 1].enabled = false;
        // stop the song
        soundtrack[track].Stop();
        // stop the animation coro
        StopCoroutine(stickerAnim);
    }

    /* Internal method for changing songs */
    void DeltaTrack(int a)
    {
        track = (track + a + soundtrack.Length) % soundtrack.Length; // to avoid overflow/underflow issues
        titleDisplay.text = soundtrackTitles[track]; // change track display name
        // reset values
        songPlaying = false;
        songPlayed = false;
        // stop the animation coro
        StopCoroutine(stickerAnim);
        // put sticker back at static frame of animation
        stickers[r].enabled = true;
        stickers[r + 1].enabled = false;
        // play the next song
        Pause();
    }

    /* Internal method for changing the loop state */
    void ToggleLoop()
    {
        loop = !loop;
        // TODO add visual indicator of loop
    }

    /* Internal method for going back to the main menu */
    void Exit()
    {
        StopAllCoroutines();
        // TODO do something
    }

    /* Called OnClick for Pause button */
    public void ClickPause()
    {
        SetPointer(0);
        Pause();
    }

    /* Called OnClick for Stop button */
    public void ClickStop()
    {
        SetPointer(1);
        Stop();
    }

    /* Called OnClick for Next button */
    public void ClickNext()
    {
        SetPointer(2);
        Stop();
        DeltaTrack(1);
    }

    /* Called OnClick for Previous button */
    public void ClickPrev()
    {
        SetPointer(3);
        Stop();
        DeltaTrack(-1);
    }

    /* Called OnClick for Loop button */
    public void ClickLoop()
    {
        SetPointer(4);
        ToggleLoop();
    }

    /* Called OnClick for Exit button */
    public void ClickExit()
    {
        SetPointer(5);
        Exit();
    }
}
