

function createCounter($element, targetNumber) {
    let currentNumber = 0;
    let increment = 1; // میزان افزایش در هر تکرار
    let isCounting = false;

    function updateCounter() {
        if (currentNumber <= targetNumber) {
            $element.text(currentNumber);
            currentNumber += increment;
            setTimeout(updateCounter, 50); // زمان تأخیر بین هر تکرار (میلی‌ثانیه)
        }
    }

    let observer = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting && !isCounting) {
                isCounting = true;
                currentNumber = 0; // تنظیم مجدد شمارنده
                updateCounter();
            }
        });
    }, { threshold: 1.0 });

    observer.observe($element[0].parentElement);
}

$(document).ready(() => {
    $('.counter-container').each(function () {
        let targetNumber = parseInt($(this).data('target'), 10);
        let $counterElement = $(this).find('.counter');
        createCounter($counterElement, targetNumber);
    });
});