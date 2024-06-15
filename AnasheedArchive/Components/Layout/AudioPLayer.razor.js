const audio = document.getElementById("audio");
const playPauseButton = document.getElementById("play-pause-button");
const progressBar = document.getElementById("progress-bar");
const currentTimeDisplay = document.getElementById("current-time");

//! audio progress bar
let isPlaying = false;

playPauseButton.addEventListener("click", () => {
    if (isPlaying) {
        audio.pause();
        playPauseButton.querySelector('svg use').setAttribute("href", "#play-fill");
    } else {
        audio.play();
        playPauseButton.querySelector('svg use').setAttribute("href", "#pause-fill");
    }
    isPlaying = !isPlaying;
});

progressBar.addEventListener("input", () => {
    audio.currentTime = (audio.duration * progressBar.value) / 100;
});

audio.addEventListener("timeupdate", () => {
    const currentTime = audio.currentTime;
    const duration = audio.duration;

    const currentMinutes = Math.floor(currentTime / 60);
    const currentSeconds = Math.floor(currentTime % 60);

    currentTimeDisplay.textContent = `${currentMinutes}:${currentSeconds < 10 ? '0' : ''}${currentSeconds}`;

    const progress = (currentTime / duration) * 100;

    // const progress = (this.value - this.min) / (this.max - this.min) * 100;
    progressBar.style.background = 'linear-gradient(to right, var(--mauve) 0%, var(--mauve) ' + progress + '%, var(--surface1) ' + progress + '%, var(--surface1) 100%)';
    progressBar.value = progress;
});


//! volume stuff
const volumeControl = document.getElementById("volume-bar");
const volumeButton = document.getElementById("volume-button");
let lastVolume = 0.5;

volumeButton.addEventListener("click", () => {
    if (volumeControl.value > 0) {
        volumeControl.value = 0;
        audio.volume = 0;
    } else {
        volumeControl.value = lastVolume;
        audio.volume = lastVolume;
    }

    SetVolumeIcon(audio.volume);
    SetVolumeBarColor(audio.volume);
});

volumeControl.addEventListener("input", () => {
    lastVolume = volumeControl.value;
    audio.volume = lastVolume;
    SetVolumeBarColor();
    SetVolumeIcon();
});

function SetVolumeBarColor(volume = -1) {
    let volPercentage = lastVolume * 100;
    if (volume >= 0) {
        volPercentage = volume * 100;
    }
    volumeControl.style.background = 'linear-gradient(to right, var(--text) 0%, var(--text) ' + volPercentage + '%, var(--surface2) ' + volPercentage + '%, var(--surface1) 100%)';
}

function SetVolumeIcon(volume = -1) {
    let currentVolume = lastVolume;
    if (volume >= 0) {
        currentVolume = volume;
    }

    if (currentVolume == 0) {
        volumeButton.querySelector('svg use').setAttribute("href", "#volume-mute-fill");
    }
    else if (currentVolume <= 0.33) {
        volumeButton.querySelector('svg use').setAttribute("href", "#volume-down-fill");
    }
    else if (currentVolume <= 0.66) {
        volumeButton.querySelector('svg use').setAttribute("href", "#volume-semi-up-fill");
    }
    else {
        volumeButton.querySelector('svg use').setAttribute("href", "#volume-up-fill");
    }
}


//! whatever else
audio.addEventListener("loadeddata", () => {
    if (audio.readyState >= 1) {
        SetAudioDuration();
    }
});

audio.addEventListener("ended", (event) => {
    playPauseButton.querySelector('svg use').setAttribute("href", "#arrow-counterclockwise");
    isPlaying = false;
});

function DisableDefaultAudioPlayer(){
    audio.controls = false;
    document.getElementById("audioPlayerControls").setAttribute("class", "");

}

function SetAudioDuration(){
    const totalTimeDisplay = document.getElementById("total-time");
    const duration = audio.duration;
    const totalMinutes = Math.floor(duration / 60);
    const totalSeconds = Math.floor(duration % 60);
    totalTimeDisplay.textContent = `${totalMinutes}:${totalSeconds < 10 ? '0' : ''}${totalSeconds}`;
}

function InitAudioPlayer() {
    audio.volume = lastVolume;
    volumeControl.value = lastVolume;
    DisableDefaultAudioPlayer();
    SetVolumeBarColor();
    SetVolumeIcon();
}

InitAudioPlayer();
