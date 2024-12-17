// Obținem elementele DOM
const uploadImage = document.getElementById("uploadImage");
const photoCanvas = document.getElementById("photoCanvas");
const ctx = photoCanvas.getContext("2d");

let loadedImage = null; // Variabilă pentru imaginea încărcată

// Funcție pentru a încărca imaginea pe canvas
uploadImage.addEventListener("change", (event) => {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            const img = new Image();
            img.onload = () => {
                // Setăm dimensiunile canvas-ului pe baza dimensiunilor imaginii
                photoCanvas.width = img.width;
                photoCanvas.height = img.height;
                // Desenăm imaginea pe canvas
                ctx.clearRect(0, 0, photoCanvas.width, photoCanvas.height);
                ctx.drawImage(img, 0, 0);
                loadedImage = img; // Salvează imaginea încărcată
            };
            img.src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});

// Funcție pentru a adăuga text pe imagine
document.getElementById("addTextBtn").addEventListener("click", () => {
    const text = prompt("Introdu textul:");
    if (text && loadedImage) {
        ctx.clearRect(0, 0, photoCanvas.width, photoCanvas.height); // Șterge imaginea existentă
        ctx.drawImage(loadedImage, 0, 0); // Redesenăm imaginea
        ctx.font = "30px Arial";
        ctx.fillStyle = "white";
        ctx.fillText(text, 50, 50); // Adăugăm textul la coordonatele (50, 50)
    }
});

// Funcție pentru a aplica efectul Alb-Negru
document.getElementById("applyGrayscale").addEventListener("click", () => {
    if (loadedImage) {
        ctx.clearRect(0, 0, photoCanvas.width, photoCanvas.height);
        ctx.drawImage(loadedImage, 0, 0);

        const imageData = ctx.getImageData(0, 0, photoCanvas.width, photoCanvas.height);
        const data = imageData.data;

        for (let i = 0; i < data.length; i += 4) {
            const r = data[i];
            const g = data[i + 1];
            const b = data[i + 2];
            const gray = (r + g + b) / 3; // Calculăm valoarea medie pentru alb-negru
            data[i] = data[i + 1] = data[i + 2] = gray; // Setăm valoarea RGB
        }

        ctx.putImageData(imageData, 0, 0); // Aplicăm modificările pe canvas
    }
});

// Funcție pentru a aplica efectul Sepia
document.getElementById("applySepia").addEventListener("click", () => {
    if (loadedImage) {
        ctx.clearRect(0, 0, photoCanvas.width, photoCanvas.height);
        ctx.drawImage(loadedImage, 0, 0);

        const imageData = ctx.getImageData(0, 0, photoCanvas.width, photoCanvas.height);
        const data = imageData.data;

        for (let i = 0; i < data.length; i += 4) {
            const r = data[i];
            const g = data[i + 1];
            const b = data[i + 2];

            // Aplica efectul sepia
            data[i] = r * 0.393 + g * 0.769 + b * 0.189; // Red
            data[i + 1] = r * 0.349 + g * 0.686 + b * 0.168; // Green
            data[i + 2] = r * 0.272 + g * 0.534 + b * 0.131; // Blue
        }

        ctx.putImageData(imageData, 0, 0); // Aplicăm modificările pe canvas
    }
});

// Funcție pentru a salva imaginea editată
document.getElementById("saveImage").addEventListener("click", () => {
    const dataUrl = photoCanvas.toDataURL("image/png");
    const link = document.createElement("a");
    link.href = dataUrl;
    link.download = "edited-image.png";
    link.click(); // Deschide dialogul de descărcare
});