// Generate a random nonce value
var nonce = Math.random().toString(36).substring(2, 10);

// Set the nonce in a meta tag in the HTML head for CSP
var meta = document.createElement('meta');
meta.httpEquiv = 'Content-Security-Policy';
//meta.content = "default-src 'self' https://fonts.googleapis.com; style-src 'self' https://fonts.googleapis.com 'nonce-" + nonce + "'";
meta.content = "default-src 'self' https://fonts.googleapis.com; script-src 'self' 'nonce-" + nonce + "'; style-src 'self' https://fonts.googleapis.com 'nonce-" + nonce + "'";
document.head.appendChild(meta);

var style = document.createElement('style');
style.setAttribute('nonce', nonce);
// Add your inline styles here

fetch('Style.css')
    .then(response => response.text())
    .then(cssText => {
        // Set the CSS content for the dynamically created <style> element
        style.textContent = cssText;

        // Append the <style> element to the <head> to apply the styles
        document.head.appendChild(style);
    })
    .catch(error => {
        console.error('Failed to load CSS file:', error);
    });