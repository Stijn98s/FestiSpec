import { EntityStatus } from '@/domain/State'

export class StateTrackable <T> {
    state: EntityStatus = EntityStatus.SavedOffline;
    entity: T;

    constructor (entity: T) {
      this.entity = entity
    }
}
