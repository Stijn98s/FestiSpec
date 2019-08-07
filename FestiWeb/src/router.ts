/* eslint-disable */
import Vue from 'vue';
import Router from 'vue-router';

import DashboardPage from './views/DashboardPage.vue';
import PlanningPage from './views/PlanningPage.vue';
import QuestionnairesPage from './views/QuestionnairesPage.vue';
import QuestionnairePage from './views/QuestionnairePage.vue';
import EventPage from './views/EventPage.vue';
import UserPage from './views/UserPage.vue';
import LoginPage from './views/LoginPage.vue';
import ResetPasswordPage from './views/ResetPasswordPage.vue';

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name: 'dashboard',
            component: DashboardPage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/planning',
            name: 'planning',
            component: PlanningPage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/questionnaire',
            name: 'questionnaires',
            component: QuestionnairesPage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/questionnaire/:id',
            name: 'questionnaire',
            component: QuestionnairePage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/event/:id',
            name: 'event',
            component: EventPage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/user',
            name: 'user',
            component: UserPage,
            meta: {
              //requiresAuth: true,
            }
        },
        {
            path: '/login',
            name: 'login',
            component: LoginPage,
            meta: {
              layout: 'Default'
              //requiresVisitor: true,
            }
        },
        {
            path: '/reset-password',
            name: 'reset-password',
            component: ResetPasswordPage,
            meta: {
              layout: 'Default'
              //requiresVisitor: true,
            }
        }
    ]
});

//# sourceMappingURL=router.js.map
