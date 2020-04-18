const recordAudio = () =>
    new Promise(async resolve => {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        const mediaRecorder = new MediaRecorder(stream);
        const audioChunks = [];

        mediaRecorder.addEventListener("dataavailable", event => {
            audioChunks.push(event.data);
        });

        const start = () => mediaRecorder.start();

        const stop = () =>
            new Promise(resolve => {
                mediaRecorder.addEventListener("stop", () => {
                    const audioBlob = new Blob(audioChunks, { type: "audio/ogg" });
                    resolve(audioBlob);
                });

                mediaRecorder.stop();
            });

        const updateProgress = (callback) => {
            const recordTime = 7000;
            const step = 100;
            const maxWidth = 250;

            const modal = document.getElementById("resultModal");
            const progressBar = document.getElementById("progress");
            const spinner = document.getElementById("spinner");
            const message = document.getElementById('message');
            const closeButton = document.getElementById("modalClose");

            closeButton.style.display = "none";

            message.style.display = "none"
            progressBar.parentElement.style.display = "block";

            $(modal).modal('show');

            var width = 0;
            const increment = (step / recordTime) * maxWidth;

            const timer = setInterval(function () {
                if (width > 100) {
                    clearInterval(timer);

                    progressBar.parentElement.style.display = "none";
                    spinner.style.display = "block";
                    callback();
                } else {
                    width = width + increment;
                    progressBar.style.width = (100 - width) + "%";
                }
            }, step);
        }

        const upload = (audioBlob, url) => {
            const userId = document.getElementById("user").value;
            const modal = document.getElementById("resultModal");

            const spinner = document.getElementById("spinner");
            const message = document.getElementById('message');

            const closeButton = document.getElementById("modalClose");

            var formData = new FormData();
            formData.append('id', userId);
            formData.append('voice', audioBlob, 'voice.ogg');

            var request = new XMLHttpRequest();
            request.open("POST", url);
            request.onreadystatechange = function () {
                if (this.readyState === 4 && this.status === 200) {
                    var result = JSON.parse(this.responseText);

                    message.innerText = result.message;

                    closeButton.style.display = "block";
                    spinner.style.display = "none";
                    message.style.display = "block";
                }
            };
            request.send(formData);
        };

        resolve({ start, stop, updateProgress, upload });
    });

const handleAction = async () => {
    const actionButton = document.getElementById('action');
    const recorder = await recordAudio();

    const url = actionButton.dataset.url;

    actionButton.disabled = true;
    recorder.start();
    recorder.updateProgress(() => {
        recorder.stop().then((audioBlob) => recorder.upload(audioBlob, url));

        actionButton.disabled = false;
    });
};
