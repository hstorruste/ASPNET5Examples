import {Component} from 'angular2/core';
import {Level} from "../models/Level.model";

@Component({
    selector: "level",
    template: `
        <h1>{{level.title}}</h1>

    `,
})
export class LevelComponent {
    public level: Level = {
        id : 1,
        background : "",
        title: "Level 1",
        seconds: 60
    };

}