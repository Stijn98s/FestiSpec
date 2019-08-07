/* eslint-disable */
import Vue from 'vue'
import Vuex from 'vuex'
import {OpenQuestion} from "@/services/ApiService";
import {EntityStatus} from "@/domain/State";

Vue.use(Vuex)

class StateTrackable <T>{
  state: EntityStatus = EntityStatus.SavedOffline;
  entity: T;

  constructor(entity: T){
      this.entity = entity;
  }
}

export interface QuestionState {
    questions: Map<Number,StateTrackable<OpenQuestion>>;
}
/*
export default new Vuex.Store<QuestionState>({
  state: {
    questions: new Map<Number, StateTrackable<OpenQuestion>>()
  },
  mutations: {
    setQuestion(state, payload: OpenQuestion){
       if(!state.questions.has(payload.id))
        state.questions.set(payload.id,new StateTrackable(payload));
      else
         state.questions.get(payload.id)!.entity = payload;
       },
  },
  actions: {

  }
})*/
