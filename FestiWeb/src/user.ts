/* eslint-disable */
export default {
    state: { 
        token: localStorage.getItem('token') || null,
    },

    getters: {
        loggedIn(state: any) {
            return state.token !== null;
        }
    },

    mutations: { 
        retrieveToken(state: any, token: string) {
            state.token = token;
            localStorage.setItem('token', token);
        },

        destroyToken(state: any) {
            state.token = null;
            localStorage.removeItem('token');

        }
    }
}