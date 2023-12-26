
let preloader = select('#preloader');
if (preloader) {
    window.addEventListener('load', () => {
        preloader.remove()
    });
}