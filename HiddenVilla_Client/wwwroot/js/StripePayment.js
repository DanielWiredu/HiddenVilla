redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51PXddoK5Npzza9StEp6WNpmGPPugpT92oleIziBObBfRy4OjIbGX618kEa3jpASH8aPft7B8LEd0rV5B368c07LN00y76hqB3p');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};