/* eslint-disable */
import Vue from 'vue'
import Vuex from 'vuex'
import {EntityStatus} from "@/domain/State";
import {OpenQuestion} from "@/services/ApiService";
import VuexPersistence from "vuex-persist";
import {StateTrackable} from "@/domain/StateTrackable";
import UserModule from "./user";
const vuexLocal = new VuexPersistence({
    storage: window.localStorage
})
Vue.use(Vuex)



 export interface QuestionState {
     questions: StateTrackable<OpenQuestion>[];
}

export default new Vuex.Store<QuestionState>({
    state: {
        questions: [],
    },
    mutations: {
        do(state){
            console.log("update");
        },
        setQuestion(state, payload: OpenQuestion){
            const found = state.questions.find(elem => elem.entity.id == payload.id);
            if(!found)
                state.questions.push(new StateTrackable(payload));
            else
                found.entity = payload;
        },
        commitResponse(state, payload: OpenQuestion[]){
            console.log("update");
            payload.forEach(elem => {
                const found = state.questions.filter(find => find.entity.id == elem.id);

                const refElem = found.splice(0,1)[0];
                found.forEach(elem => state.questions.splice(state.questions.indexOf(elem)));
                if(!refElem || refElem.state != EntityStatus.SavedLocal){
                    const elem2 = new StateTrackable(elem);
                    state.questions.push(elem2);
                    elem2.state = EntityStatus.SavedOffline;
                    console.log("add element")
                }
            });
        },
        pending(state, payload: OpenQuestion){
            const elem = state.questions.find(elem => elem.entity.id == payload.id);
            if (elem) {
                elem.entity = payload;
                elem.state = EntityStatus.SavedLocal;
            }
        },
        completed(state, payload: OpenQuestion){
            const elem = state.questions.find(elem => elem.entity.id == payload.id);
            if (elem) {
                elem.entity = payload;
                elem.state = EntityStatus.SavedOffline;
            }
        }
    },
    getters: {
      getQuestions(state){
        return Array.from(state.questions.map(elem => elem));
      }
    },
    plugins: [vuexLocal.plugin],
    modules: {
        user: UserModule
    }
})




